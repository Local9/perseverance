import { writable } from "svelte/store";
import { create } from "../data/create.data";
import { fetchNui } from "@utils/fetchNui";

export const genders = writable<any[]>([]);
export const ethnicities = writable<any[]>([]);
export const addresses = writable<any[]>([]);
export const postals = writable<any[]>([]);

function getList(data: any, type: string) {
  const filteredList = data.filter((value) => value.type === type);
  if (filteredList.length > 0) {
    const values = filteredList[0].values;
    return values.map((item) => {
      return {
        label: item.value,
        value: item.id,
      };
    });
  }
}

function getGenders(data: any) {
  genders.set(getList(data, "GENDER"))
}

function getEthnicities(data: any) {
  ethnicities.set(getList(data, "ETHNICITY"));
}

export function GetServerProps() {
  if (import.meta.env.DEV) {
    getEthnicities(create);
    getGenders(create);
    return;
  }
  fetchNui("getServerProps")
  .then((returnData) => {
    getEthnicities(returnData);
    getGenders(returnData);
  })
  .catch((e) => {});
}

export function GetAddresses() {
  if (import.meta.env.DEV) {
    addresses.set(create.addresses);
    return;
  }
  fetchNui("getAddresses")
  .then((returnData) => {
    addresses.set(returnData);
  })
  .catch((e) => {});
}