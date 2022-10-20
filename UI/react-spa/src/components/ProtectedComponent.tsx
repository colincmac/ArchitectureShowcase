import React from "react";
import {
  MsalProvider,
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
  MsalAuthenticationTemplate,
} from "@azure/msal-react";
import { useLogin } from "../hooks/useIdentity";

export const ProtectedComponent = ({ children }: React.PropsWithChildren) => {
  const { result, error: msalError, accounts } = useLogin();

  return (
    <>
      <AuthenticatedTemplate>{children}</AuthenticatedTemplate>
      <UnauthenticatedTemplate>
        <p>No users are signed in!</p>
      </UnauthenticatedTemplate>
    </>
  );
};
