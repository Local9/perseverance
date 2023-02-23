import { writable } from "svelte/store";
import { fetchNui } from "@utils/fetchNui";
import { user } from "../data/user.data";


export const storeUser = writable<any>({});

export function getUser() {
  if (import.meta.env.DEV) {
    storeUser.set(user);
    return;
  }

  fetchNui("getUser")
    .then((returnData) => {
      storeUser.set(returnData);
    })
    .catch((e) => {});
}