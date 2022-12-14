import { NavigationClient } from "@azure/msal-browser";
import { NavigateFunction } from "react-router-dom";

/**
 * This is an example for overriding the default function MSAL uses to navigate to other urls in your webpage
 */
export class CustomNavigationClient extends NavigationClient {
  constructor(navigate: NavigateFunction) {
    super();
    this.navigate = navigate;
  }

  navigate: NavigateFunction;
  /**
   * Navigates to other pages within the same web application
   * You can use the useNavigate hook provided by react-router-dom to take advantage of client-side routing
   * @param url
   * @param options
   */
  async navigateInternal(url: any, options: any) {
    const relativePath = url.replace(window.location.origin, "");
    console.log(url);
    if (options.noHistory) {
      this.navigate(relativePath, { replace: true });
    } else {
      this.navigate(relativePath);
    }

    return false;
  }
}
