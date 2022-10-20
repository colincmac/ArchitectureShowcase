import axios from "axios";
import {
  HubConnection,
  HubConnectionState,
  HubConnectionBuilder,
} from "@microsoft/signalr";
import { useCallback, useEffect, useState } from "react";

type UseSignalRHubProps = {
  accessTokenFactory?: () => string | Promise<string>;
};

/**
 * Start/Stop the provided hub connection (on connection change or when the component is unmounted)
 * @param {HubConnection} hubConnection The signalR hub connection
 * @return {HubConnection} the current signalr connection
 * @return {any} the signalR error in case the start does not work
 */
export function useSignalRHub({ accessTokenFactory }: UseSignalRHubProps) {
  const [error, setError] = useState();
  const [hubConnection, setHubConnection] = useState<HubConnection>();
  const [hubConnectionState, setHubConnectionState] =
    useState<HubConnectionState>(
      hubConnection ? hubConnection.state : HubConnectionState.Disconnected
    );

  const getSignalRConnection = useCallback(async () => {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:7193/api", {
        accessTokenFactory,
        headers: {
          "x-ms-client-principal-id": "14dffacb-74d1-46cd-b49a-3a1459590573",
        },
      })
      .withAutomaticReconnect()
      .build();

    setHubConnection(connection);
  }, [accessTokenFactory]);

  const onStateUpdatedCallback = useCallback(
    () =>
      setHubConnectionState(
        hubConnection ? hubConnection?.state : HubConnectionState.Disconnected
      ),
    [hubConnection]
  );

  useEffect(() => {
    setError(undefined);

    if (!hubConnection) {
      getSignalRConnection();
      return;
    }

    hubConnection.onclose(onStateUpdatedCallback);
    hubConnection.onreconnected(onStateUpdatedCallback);
    hubConnection.onreconnecting(onStateUpdatedCallback);

    if (hubConnection.state === HubConnectionState.Disconnected) {
      const startPromise = hubConnection
        .start()
        .then(onStateUpdatedCallback)
        .catch((reason) => setError(reason));
      onStateUpdatedCallback();

      return () => {
        startPromise.then(() => {
          hubConnection.stop();
        });
      };
    }

    return () => {
      hubConnection.stop();
    };
  }, [hubConnection]);

  return { hubConnectionState, error };
}
