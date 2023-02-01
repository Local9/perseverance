<script lang="ts">
  import { storeCitizens, getCitizens } from "../../store/citizen";
  import { onMount } from "svelte";
  import type { ICitizen } from "src/@types/citizen";
  import Citizen from "./Citizen.svelte";

  let myCitizens: ICitizen[] = [];

  storeCitizens.subscribe((value: ICitizen[]) => {
    myCitizens = value;
  });

  onMount(async () => {
    getCitizens();
  });
</script>

<div>
  <h2>Citizens</h2>
  {#if myCitizens.length === 0}
    <p>No citizens found</p>
  {:else}
    <div class="citizens">
      {#each myCitizens as citizen}
        <Citizen {citizen} />
      {/each}
    </div>
  {/if}
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
