<script lang="ts">
  import Button from "@components/form/Button.svelte";
  import Modal from "@shared/Modal.svelte";
  import TextField from "@components/form/TextField.svelte";
  import type { ICitizen } from "src/@types/citizen";
  import Select from "@components/form/inputs/Select.svelte";
  import { genders, ethnicities, getServerProps, getAddresses, addresses } from "@store/application";
  import { getCitizens, saveCitizen, getCitizen } from "@store/citizen";
  import { afterUpdate } from "svelte";

  export let showModal: boolean = false;
  export let citizen: ICitizen = {
    id: undefined,
    name: undefined,
    surname: undefined,
    imageId: undefined,
    imageBlurData: undefined,
    socialSecurityNumber: undefined,
    fullname: undefined,
    dateOfBirth: undefined,
    gender: undefined,
    hairColor: undefined,
    eyeColor: undefined,
    height: undefined,
    weight: undefined,
    address: undefined,
    postal: undefined,
    ethnicity: undefined,
  };

  let citizenCopy: ICitizen = { ...citizen };
  let citizenGenders: any[] = [];
  let citizenEthnicities: any[] = [];
  let citizenAddresses: any;

  let setup = false;

  $: getServerProps();
  $: getAddresses();

  $: if (citizen.id && !setup) {
    getCitizen(citizen.id).then(
      (data: any) => {
        citizenCopy = { ...data.citizen };
      },
      (error: any) => {
        console.error(error);
        citizenCopy = { ...citizen };
      }
    );
    setup = true;
  }

  genders.subscribe((data: any) => {
    citizenGenders = data;
  });

  ethnicities.subscribe((data: any) => {
    citizenEthnicities = data;
  });

  addresses.subscribe((data: any) => {
    citizenAddresses = data;
  });

  function onClickSaveCitizen() {
    if (import.meta.env.DEV) {
      console.log(citizenCopy, "onClickSaveCitizen");
    }
    // Save back to store
    saveCitizen(citizenCopy).then((response) => {
      if (response.success) {
        getCitizens();
        showModal = false;
      }
    });
  }
</script>

<Modal>
  <div class="flex flex-row border-b border-solid border-b-white">
    <h2 class="flex-grow text-2xl">{citizenCopy.id ? "Edit Citizen" : "Create Citizen"}</h2>
  </div>
  <form on:submit|preventDefault={onClickSaveCitizen}>
    <div class="grid grid-cols-3 gap-0.5 gap-x-2">
      <TextField label="Name" placeholder="John" type="text" id="name" bind:value={citizenCopy.name} required />
      <TextField label="Surname" placeholder="Doe" type="text" id="surname" bind:value={citizenCopy.surname} required />
      <TextField label="Date of Birth" type="text" id="dateofbirth" bind:value={citizenCopy.dateOfBirth} required />
      <TextField label="Hair Color" placeholder="Dark Brown" type="text" id="haircolor" bind:value={citizenCopy.hairColor} required />
      <TextField label="Eye Color" placeholder="Light Blue" type="text" id="eyecolor" bind:value={citizenCopy.eyeColor} required />
      <div />
      <TextField label="Weight" type="text" placeholder="00.00" id="weight" optionalLabel="kg" bind:value={citizenCopy.weight} required />
      <TextField label="Height" type="text" placeholder="00.00" id="height" optionalLabel="cm" bind:value={citizenCopy.height} required />
      <div />
      <Select label="Gender" placeholder="Male" type="text" id="gender" bind:value={citizenCopy.gender} options={citizenGenders} required />
      <Select label="Ethnicity" placeholder="White" type="text" id="ethnicity" bind:value={citizenCopy.ethnicity} options={citizenEthnicities} required />
      <Select class="col-span-2" label="Address" type="text" id="address" bind:value={citizenCopy.address} options={citizenAddresses} required />
      <!-- <Select label="Postal" type="text" id="postal" bind:value={citizenCopy.postal} /> -->
    </div>
    <div class="grid grid-cols-2 gap-0.5 gap-x-2">
      <Button type="button" on:click={() => (showModal = false)}>Cancel</Button>
      <Button type="submit">{citizenCopy?.id ? "Save Edited Citizen" : "Create Citizen"}</Button>
    </div>
  </form>
</Modal>
