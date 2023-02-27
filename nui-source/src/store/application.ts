import { writable } from "svelte/store";
import { create } from "../data/create.data";

export const genders = writable<any[]>([]);
export const ethnicities = writable<any[]>([]);
export const addresses = writable<any[]>([]);
export const postals = writable<any[]>([]);

function getList(type: string) {
  const filteredList = create.pageProps.values.filter((value) => value.type === type);
  if (filteredList.length > 0) {
    const values = filteredList[0].values;
    return values.map((item) => {
      return {
        label: item.value,
        value: item.value,
      };
    });
  }
}

export function getGenders() {
  if (import.meta.env.DEV) {
    genders.set(getList("GENDER"))
  }
}

export function getEthnicities() {
  if (import.meta.env.DEV) {
    ethnicities.set(getList("ETHNICITY"));
  }
}