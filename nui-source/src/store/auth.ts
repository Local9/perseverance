import { writable } from "svelte/store";

export const isAuthenticated = writable(false);

export function authenticate(username: string, password: string) {
  fetchNui("authenticate", { 0: username, 1: password })
    .then((returnData) => {
      isAuthenticated.set(true);
    })
    .catch((e) => {});
}