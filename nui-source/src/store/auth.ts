import { writable } from "svelte/store";
import { fetchNui } from "@utils/fetchNui";

export const isAuthenticated = writable(false);

export function authenticate(username: string, password: string) {
  if (import.meta.env.DEV) {
    isAuthenticated.set(true);
    return;
  }

  fetchNui("authenticate", { 0: username, 1: password })
    .then((returnData) => {
      if (returnData.success) {
        isAuthenticated.set(true);
      }
    })
    .catch((e) => {});
}

export function register(username: string, password: string) {
  if (import.meta.env.DEV) {
    isAuthenticated.set(true);
    return;
  }
  fetchNui("register", { 0: username, 1: password })
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