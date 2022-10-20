import { IPublicClientApplication } from "@azure/msal-browser";
import React from "react";
import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import LiveDataGrid from "./components/LiveDataGrid";

export const createRouterWithIdentity = (pca: IPublicClientApplication) =>
  createBrowserRouter([
    {
      path: "/",
      element: <App pca={pca} />,
      children: [
        {
          path: "/",
          element: <LiveDataGrid />,
        },
      ],
    },
  ]);
