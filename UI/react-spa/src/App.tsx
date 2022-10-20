import React, { useEffect, useState } from "react";
import {
  AuthenticatedTemplate,
  MsalProvider,
  UnauthenticatedTemplate,
} from "@azure/msal-react";

import { IPublicClientApplication } from "@azure/msal-browser";
import { ProtectedComponent } from "./components/ProtectedComponent";
import { ClientSideNav } from "./components/ClientSideNav";
import { Outlet } from "react-router-dom";

export default function App({ pca }: { pca: IPublicClientApplication }) {
  return (
    <ClientSideNav pca={pca}>
      <MsalProvider instance={pca}>
        <ProtectedComponent>
          <Outlet />
        </ProtectedComponent>
      </MsalProvider>
    </ClientSideNav>
  );
}
