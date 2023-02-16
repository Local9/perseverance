<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import Icon from '@iconify/svelte';
  import { authenticate } from '@store/auth';
  import Login from './Login.svelte';
  import Register from './Register.svelte';

  export let showHero: boolean = true;
  export let width: number = 700;

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

<article class="grid" style="width: {width}px">
  {#if page === 'login'}
    <div>
      <hgroup>
        <Icon icon="line-md:account" width="100" />
      </hgroup>
      <Login />
      <Button on:click={handleOnClick} variant="default" size="md"
        >Don't have an account?<br />Register here</Button
      >
    </div>
  {:else}
    <div>
      <hgroup>Register</hgroup>
      <Register />
      <Button on:click={handleOnClick} variant="default" size="md"
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
