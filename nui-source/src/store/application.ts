import { writable } from "svelte/store";
import { createData } from "../data/create.data";
import { addressData } from "../data/address.data";
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

function createAddressList(data: any) {
  const lst: any[] = [];
  data.forEach((element) => {
    const county = element.county;
    const postal = element.postal;
    const id = element.id;
    const address = element.value;
    const addressId = address.id;
    const addressLabel = address.value;

    lst.push({
      label: `${addressLabel}, ${postal} ${county}`,
      value: `${addressLabel}, ${postal} ${county}`,
    });
  });
  postals.set(createPostalList(data));
  return lst;
}

function createPostalList(data: any) {
  const lst: any[] = [];
  data.forEach((element) => {
    const county = element.county;
    const postal = element.postal;

    lst.push({
      label: `${postal} ${county}`,
      value: `${postal}`,
    });
  });
  return lst;
}

export function getServerProps() {
  if (import.meta.env.DEV) {
    ethnicities.set(getList(createData, "ETHNICITY"));
    genders.set(getList(createData, "GENDER"));
    return;
  }
  fetchNui("getServerProps")
    .then((returnData) => {
      ethnicities.set(getList(returnData, "ETHNICITY"));
      genders.set(getList(returnData, "GENDER"));
    })
    .catch((e) => {});
}

export function getAddresses() {
  if (import.meta.env.DEV) {
    addresses.set(createAddressList(addressData));
    return;
  }
  fetchNui("getAddresses")
    .then((returnData) => {
      addresses.set(createAddressList(returnData));
    })
    .catch((e) => {});
}
