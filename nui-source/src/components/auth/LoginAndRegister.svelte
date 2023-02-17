<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import { authenticate } from '@store/auth';
  import Login from './Login.svelte';
  import Register from './Register.svelte';

  export let showHero: boolean = true;

  let username: string = '';
  let password: string = '';

  let page: string = 'login';

  const handleOnClick = () => {
    page = page === 'login' ? 'register' : 'login';
  };

  const handleClientData = () => {
    authenticate(username, password);
  };
</script>

<article class="{$$restProps.class} grid">
  {#if page === 'login'}
    <div>
      <hgroup>
        <!-- Replace with image from CAD -->
        <img
          src="https://avatars.githubusercontent.com/u/91481975?s=200&v=4"
          alt="Logo"
          class="rounded-full mb-4"
        />
      </hgroup>
      <Login class="w-full" />
      <Button
        on:click={handleOnClick}
        variant="default"
        size="md"
        class="flex items-center justify-center w-full gap-3 mt-5"
        >Don't have an account?<br />Register here</Button
      >
    </div>
  {:else}
    <div>
      <hgroup class="dark: text-white">Register</hgroup>
      <Register class="w-full" />
      <Button
        on:click={handleOnClick}
        variant="default"
        size="md"
        class="flex items-center justify-center w-full gap-3 mt-5"
        >Go back</Button
      >
    </div>
  {/if}
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
