import { IPublicClientApplication } from "@azure/msal-browser";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { CustomNavigationClient } from "../utils/CustomNavigationClient";

export const ClientSideNav = ({
  pca,
  children,
}: React.PropsWithChildren<{ pca: IPublicClientApplication }>) => {
  const navigate = useNavigate();
  const navigationClient = new CustomNavigationClient(navigate);
  pca.setNavigationClient(navigationClient);

  // react-router-dom v6 doesn't allow navigation on the first render - delay rendering of MsalProvider to get around this limitation
  const [firstRender, setFirstRender] = useState(true);
  useEffect(() => {
    setFirstRender(false);
  }, []);

  if (firstRender) {
    return null;
  }

  return <>{children}</>;
};
