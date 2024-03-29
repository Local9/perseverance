import { writable } from "svelte/store";
import { fetchNui } from "@utils/fetchNui";
import { citizensData } from "../data/citizens.data";
import type { ICitizen } from "../@types/citizen";

export const storeCitizens = writable<ICitizen[]>([]);
export const storeCitizen = writable<ICitizen>({});

export function setCitizen(citizen: ICitizen) {
  storeCitizen.set(citizen);

  fetchNui("setCitizen", { id: citizen.id, fullname: citizen.fullname })
    .then((returnData) => {
      if (returnData.success) {
        
      }
    })
    .catch((e) => {});
}

export function getCitizens() {
  if (import.meta.env.DEV) {
    storeCitizens.set(citizensData.citizens);
    return;
  }

  fetchNui("getCitizens")
    .then((returnData) => {
      if (returnData.success) {
        storeCitizens.set(returnData.citizens);
      }
    })
    .catch((e) => {});
}