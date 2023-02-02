<script lang="ts">
  // import { fetchNui } from "../../utils/fetchNui";
  import Icon from "@iconify/svelte";
  import { authenticate } from "@store/auth";

  let username: string = "";
  let password: string = "";

  const handleClientData = () => {
    authenticate(username, password);
  };
</script>

<main class="container">
  <article class="grid">
    <div>
      <hgroup>
        <Icon icon="line-md:account" width="100" />
      </hgroup>
      <form on:submit|preventDefault={handleClientData}>
        <input
          bind:value={username}
          type="text"
          name="login"
          placeholder="Login"
          aria-label="Login"
          autocomplete="nickname"
          required
        />
        <input
          bind:value={password}
          type="password"
          name="password"
          placeholder="Password"
          aria-label="Password"
          autocomplete="current-password"
          required
        />
        <button type="submit" class="contrast">Login</button>
      </form>
    </div>
    <div />
  </article>
</main>

<style type="scss">
  main {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100%;
  }

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
    background-image: url("./assets/images/login/fivem.jpg");
    background-position: center;
    background-size: cover;
  }

  @media (min-width: 992px) {
    .grid > div:nth-of-type(2) {
      display: block;
    }
  }
</style>
