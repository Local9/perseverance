import { writable } from "svelte/store";
import { fetchNui } from "@utils/fetchNui";
import { licenseData } from "../data/license.data";


export const storeLicense = writable<any[]>([]);

export function getLicense() {
  if (import.meta.env.DEV) {
    storeLicense.set(license);
    return;
  }

  fetchNui("getLicense")
    .then((returnData) => {
      storeLicense.set(returnData);
    })
    .catch((e) => {});
}