import { writable } from "svelte/store";
import { fetchNui } from "@utils/fetchNui";
import { failure } from "@store/toast";

export const isAuthenticated = writable(false);

export function authenticate(username: string, password: string) : boolean {
  if (import.meta.env.DEV) {
    isAuthenticated.set(true);
    return true;
  }

  fetchNui("authenticate", { username, password })
    .then((returnData) => {
      if (returnData.success) {
        isAuthenticated.set(true);
        return true;
      } else {
        // todo custom classes as toast is affected by pico css
        failure("Invalid username or password");
        return false;
      }
    })
    .catch((e) => { });
}

export function register(username: string, password: string, passwordConfirm: string, registrationCode: string) {
  if (import.meta.env.DEV) {
    isAuthenticated.set(true);
    return;
  }
  fetchNui("register", { username, password, passwordConfirm, registrationCode })
    .then((returnData) => {
      if (returnData.success) {

      }
    })
    .catch((e) => {});
}

export function logout() {
    if (import.meta.env.DEV) {
    isAuthenticated.set(false);
    return;
  }
  fetchNui("logout", { 0: username, 1: password })
    .then((returnData) => {
      if (returnData.success) {

      }
    })
    .catch((e) => {});
}