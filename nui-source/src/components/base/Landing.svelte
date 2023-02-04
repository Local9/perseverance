<script lang="ts">
  import { fade } from 'svelte/transition';
  import LoginAndRegister from '@items/LoginAndRegister.svelte';
</script>

<main transition:fade>
  <div>
    <LoginAndRegister showHero={false} width={400} />
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
    background-color: rgba(var(--alt-background-color-rgb), 0.5);
  }
  div:nth-child(2) {
    grid-area: right;
  }
</style>
