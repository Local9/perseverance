<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import Modal from '@shared/Modal.svelte';
  import TextField from '@components/form/TextField.svelte';
  import type { ICitizen } from 'src/@types/citizen';
  import Select from '@components/form/inputs/Select.svelte';
  import { genders, ethnicities, getEthnicities, getGenders } from '@store/application';

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

  $: getGenders();
  $: getEthnicities();

  genders.subscribe((data: any) => {
    citizenGenders = data;
  });

  ethnicities.subscribe((data: any) => {
    citizenEthnicities = data;
  });

  function onClickSaveCitizen() {
    // Save back to store
    if (citizenCopy.id) {
      console.log('update citizen');
    } else {
      console.log('create citizen');
    }
    citizen = { ...citizenCopy };
    showModal = false;
  }
</script>

<Modal>
  <div class="flex flex-row border-b border-solid border-b-white">
    <h2 class="flex-grow text-2xl">{citizenCopy.id ? 'Edit Citizen' : 'Create Citizen'}</h2>
  </div>
  <div class="grid grid-cols-3 gap-0.5 gap-x-2">
    <TextField label="Name" placeholder="John" type="text" id="name" bind:value={citizenCopy.name} />
    <TextField label="Surname" placeholder="Doe" type="text" id="surname" bind:value={citizenCopy.surname} />
    <TextField label="Date of Birth" type="date" id="dateofbirth" bind:value={citizenCopy.dateOfBirth} />
    <TextField label="Hair Color" placeholder="Dark Brown" type="text" id="haircolor" bind:value={citizenCopy.hairColor} />
    <TextField label="Eye Color" placeholder="Light Blue" type="text" id="eyecolor" bind:value={citizenCopy.eyeColor} />
    <div />
    <TextField label="Weight" type="text" placeholder="00.00" id="weight" optionalLabel="kg" bind:value={citizenCopy.weight} />
    <TextField label="Height" type="text" placeholder="00.00" id="height" optionalLabel="cm" bind:value={citizenCopy.height} />
    <div />
    <Select label="Gender" placeholder="Male" type="text" id="gender" bind:value={citizenCopy.gender} options={citizenGenders} />
    <Select label="Ethnicity" placeholder="White" type="text" id="ethnicity" bind:value={citizenCopy.ethnicity} options={citizenEthnicities} />
    <Select class="col-span-2" label="Address" type="text" id="address" bind:value={citizenCopy.address} />
    <Select label="Postal" type="text" id="postal" bind:value={citizenCopy.postal} />
  </div>
  <div class="grid grid-cols-2 gap-0.5 gap-x-2">
    <Button on:click={() => (showModal = false)}>Cancel</Button>
    <Button on:click={onClickSaveCitizen}>{citizenCopy?.id ? 'Save Edited Citizen' : 'Create Citizen'}</Button>
  </div>
</Modal>
