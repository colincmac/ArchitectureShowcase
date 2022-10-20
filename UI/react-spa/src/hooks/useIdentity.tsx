import React, { useEffect } from "react";

import { useMsal, useMsalAuthentication } from "@azure/msal-react";
import {
  InteractionType,
  InteractionRequiredAuthError,
  InteractionStatus,
} from "@azure/msal-browser";

export const useLogin = () => {
  const { login, result, error } = useMsalAuthentication(
    InteractionType.Silent
  );
  const { accounts } = useMsal();

  useEffect(() => {
    if (error instanceof InteractionRequiredAuthError) {
      login(InteractionType.Redirect, {
        loginHint: "cmccullough@microsoft.com",
        scopes: ["User.Read"],
      });
    }
  }, [error, login]);

  return { accounts, result, error };
};

export const useAccessToken = () => {
  const { login, result, error } = useMsalAuthentication(
    InteractionType.Silent
  );
  const { accounts } = useMsal();

  useEffect(() => {
    if (error instanceof InteractionRequiredAuthError) {
      login(InteractionType.Redirect, {
        loginHint: "cmccullough@microsoft.com",
        scopes: ["User.Read"],
      });
    }
  }, [error, login]);

  return { accounts, result, error };
};
