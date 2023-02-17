<script lang="ts">
  import { fade } from 'svelte/transition';
  import LoginAndRegister from '@auth/LoginAndRegister.svelte';
  import { isAuthenticated } from '@store/auth';
  import Citizens from '@components/citizen/Citizens.svelte';

  let isAuthed = false;

  isAuthenticated.subscribe((authenticated: boolean) => {
    isAuthed = authenticated;
  });
</script>

<main transition:fade>
  <div>
    {#if !isAuthed}
      <LoginAndRegister showHero={false} class="w-full" />
    {:else}
      <Citizens showNameplates={false} />
    {/if}
  </div>
  <div />
</main>

<style type="scss">
  main {
    height: 100%;
    display: grid;
    grid-template-columns: 1fr 3fr;
    grid-template-rows: 1fr;
    grid-template-areas: 'left right';
  }
  div {
    height: 100%;
  }
  div:nth-child(1) {
    grid-area: left;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-color: rgba(var(--background-color), 0.75);
    min-width: 400px;
  }
  div:nth-child(2) {
    grid-area: right;
  }
</style>
