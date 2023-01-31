<script lang="ts">
  import { fly, fade } from "svelte/transition";
  import { cubicIn, cubicOut, quadOut } from "svelte/easing";

  import { isAuthenticated } from "../../store/auth";
  import Login from "./Login.svelte";
  import TabletScreen from "./TabletScreen.svelte";

  let isAuthed = false;

  isAuthenticated.subscribe((authenticate) => {
    isAuthed = authenticate;
  });
</script>

<div
  class="tablet"
  in:fly={{ y: 1000, duration: 500, easing: cubicOut }}
  out:fly={{ y: 1000, duration: 500, easing: cubicIn }}
>
  {#if !isAuthed}
    <div transition:fade>
      <Login />
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
    background-color: var(--thumb-background-color);
    border-radius: calc(var(--sizeVar) / 24);
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    box-shadow: 0 0 0 calc(var(--sizeVar) / 200) #9d9ea0;
  }
</style>
