<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import { registerInBrowser } from '@store/auth';
  import Login from './Login.svelte';

  export let showHero: boolean = true;
</script>

<article class="{$$restProps.class} grid">
  <div>
    <hgroup>
      <!-- Replace with image from CAD -->
      <img src="https://raw.githubusercontent.com/SnailyCAD/website/main/static/logos/SnailyCAD_logo.svg" alt="Logo" class="w-40 rounded-3xl mb-4" />
    </hgroup>
    <Login class="w-full" />
    <Button on:click={registerInBrowser} variant="default" size="md" class="flex items-center justify-center w-full gap-3 mt-5">Don't have an account?<br />Register here (opens browser)</Button>
  </div>
  {#if showHero}
    <div />
  {/if}
</article>

<style type="scss">
  article {
    padding: 0;
    overflow: hidden;

    div:nth-of-type(1) {
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 2rem;
    }
  }

  .grid {
    display: grid;
  }

  article div:nth-of-type(2) {
    display: none;
    background-color: #374956;
    background-image: url('./assets/images/login/fivem.jpg');
    background-position: center;
    background-size: cover;
  }

  @media (min-width: 992px) {
    .grid > div:nth-of-type(2) {
      display: block;
    }
  }
</style>
