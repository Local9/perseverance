<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import Modal from '@shared/Modal.svelte';
  import Icon from '@iconify/svelte';
  import TextField from '@components/form/TextField.svelte';

  export let showCreateCitizenModal: boolean = false;
</script>

<Modal>
  <div class="flex flex-row">
    <span class="text-xl"> Create Citizen </span>
    <span class="ml-auto" on:click={() => (showCreateCitizenModal = false)}>
      <Icon icon="carbon:close" width="24" height="24" />
    </span>
  </div>
  <div class="grid grid-cols-4 gap-0.5 gap-x-2">
    <TextField label="Name" placeholder="John" type="text" id="name" />
    <TextField label="Surname" placeholder="Doe" type="text" id="surname" />
    <TextField label="Date of Birth" type="date" id="dateofbirth" />
    <TextField label="Gender" placeholder="Male" type="text" id="gender" optionalLabel="select" />
    <TextField label="Hair Color" placeholder="Dark Brown" type="text" id="haircolor" />
    <TextField label="Eye Color" placeholder="Light Blue" type="text" id="eyecolor" />
    <TextField label="Weight" placeholder="00.00" type="text" id="weight" optionalLabel="kg" />
    <TextField label="Height" placeholder="00.00" type="text" id="height" optionalLabel="cm" />
    <TextField class="col-span-3" label="Address" type="text" id="address" optionalLabel="select" />
    <TextField label="Postal" type="text" id="postal" optionalLabel="select" />
  </div>
  <div class="flex flex-row">
    <Button class="flex-grow m-1" on:click={() => (showCreateCitizenModal = false)}>Cancel</Button>
    <Button class="flex-grow m-1">Submit</Button>
  </div>
</Modal>
