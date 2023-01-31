<script lang="ts">
  import Login from "./Login.svelte";
</script>

<div class="tablet">
  <!-- if not logged in, show login -->
  <Login />
  <!-- if logged in, show the tablet options -->
  <div>options</div>
</div>

<style type="scss">
  // initial inspiration taken from JL-Laptop: https://github.com/JustLazzy/jl-laptop
  // honestly is some amazing work and inspired this project
  .tablet {
    overflow: hidden;
    width: 90%;
    height: 90%;
    bottom: -40%;
    left: 50%;
    transform: translate(-50%, -50%);
    position: absolute;
    background-color: var(--thumb-background-color);
    border: 5px solid var(--card-border-color);
    border-radius: 5px;
    background-size: cover;
    background-position: center;
    opacity: 1;
    transition: 1s;
    z-index: -1;
  }
</style>
