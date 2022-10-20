import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import {
  PublicClientApplication,
  EventType,
  AuthenticationResult,
} from "@azure/msal-browser";
import { msalConfig } from "./config/authConfig";
import { Router, RouterProvider } from "react-router-dom";
import { createRouterWithIdentity } from "./routes";

export const msalInstance = new PublicClientApplication(msalConfig);

// Default to using the first account if no account is active on page load
if (
  !msalInstance.getActiveAccount() &&
  msalInstance.getAllAccounts().length > 0
) {
  // Account selection logic is app dependent. Adjust as needed for different use cases.
  msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
}

// Optional - This will update account state if a user signs in from another tab or window
msalInstance.enableAccountStorageEvents();

msalInstance.addEventCallback((event) => {
  if (event.eventType === EventType.LOGIN_SUCCESS) {
    const account = (event?.payload as AuthenticationResult).account;
    msalInstance.setActiveAccount(account);
  }
});

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

const routerWithIdentity = createRouterWithIdentity(msalInstance);
root.render(
  <React.StrictMode>
    <RouterProvider router={routerWithIdentity} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
