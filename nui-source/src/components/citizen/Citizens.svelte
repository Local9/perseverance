<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import { storeCitizens, getCitizens } from '@store/citizen';
  import { onMount } from 'svelte';
  import type { ICitizen } from '../../@types/citizen';
  import Citizen from './Citizen.svelte';
  import { logout } from '@store/auth';

  export let showNameplates: boolean = true;
  let myCitizens: ICitizen[] = [];

  storeCitizens.subscribe((value: ICitizen[]) => {
    myCitizens = value;
  });

  onMount(async () => {
    getCitizens();
  });
</script>

<div>
  {#if myCitizens.length === 0}
    <p>No citizens found</p>
    <Button
      class="rounded-md disabled:opacity-60 disabled:cursor-not-allowed transition-colors"
      >Create Citizen</Button
    >
  {:else}
    <div class="citizens">
      {#each myCitizens as citizen}
        <Citizen {citizen} showNameplate={showNameplates} />
      {/each}
      <Button>Create Citizen</Button>
    </div>
  {/if}
  <Button on:click={logout}>Logout</Button>
</div>

<style lang="scss">
  .citizens {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
    grid-gap: var(--grid-spacing-vertical)
      calc(var(--grid-spacing-horizontal) + 0.5rem);

    justify-content: center;
    align-items: center;
    height: 100%;
  }
</style>
