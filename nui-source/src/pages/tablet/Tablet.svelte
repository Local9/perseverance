<script lang="ts">
  import { fly, fade } from 'svelte/transition';
  import { cubicIn, cubicOut } from 'svelte/easing';
  import { isAuthenticated } from '@store/auth';
  import TabletScreen from './TabletScreen.svelte';
  import LoginAndRegister from '@auth/LoginAndRegister.svelte';

  let isAuthed = false;

  isAuthenticated.subscribe((authenticated: boolean) => {
    isAuthed = authenticated;
  });
</script>

<div
  class="tablet"
  in:fly={{ y: 1000, duration: 500, easing: cubicOut }}
  out:fly={{ y: 1000, duration: 500, easing: cubicIn }}
>
  {#if !isAuthed}
    <div transition:fade>
      <LoginAndRegister />
    </div>
  {:else}
    <div transition:fade>
      <TabletScreen />
    </div>
  {/if}
  <!-- if not logged in, show login -->
  <!-- if logged in, show the tablet options -->
</div>

<style type="scss">
  // initial inspiration taken from JL-Laptop: https://github.com/JustLazzy/jl-laptop
  // honestly is some amazing work and inspired this project
  .tablet,
  .tablet div {
    position: absolute;
    width: 85vw;
    height: calc(var(--sizeVar) * 0.87);
    background-color: var(--alt-background-color);
    border-radius: calc(var(--sizeVar) / 48);
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    box-shadow: 0 0 0 calc(var(--sizeVar) / 200) #9d9ea0;
  }
</style>
