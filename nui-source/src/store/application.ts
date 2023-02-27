import { writable } from "svelte/store";

export const genders = writable<any[]>([]);
export const addresses = writable<any[]>([]);
export const postals = writable<any[]>([]);