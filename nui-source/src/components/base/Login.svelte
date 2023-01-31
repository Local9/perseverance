<script lang="ts">
  // import { fetchNui } from "../../utils/fetchNui";
  import Icon from "@iconify/svelte";
  import { authenticate, isAuthenticated } from "../../store/auth";

  let username: string;
  let password: string;

  const handleClientData = () => {
    console.log();
    authenticate(username, password);
  };

  // const closeDialog = (e: KeyboardEvent) => {
  //   if (e.type === "keydown" && e.key !== "Escape") return;
  //   visibility.set(false);
  //   fetchNui("hideUI");
  // };
</script>

<div>
  <article>
    <Icon icon="line-md:account" width="100" />
    <form on:submit|preventDefault={handleClientData}>
      <input type="text" placeholder="Username" bind:value={username} />
      <input type="password" placeholder="Password" bind:value={password} />
      <button type="submit">Login</button>
    </form>
  </article>
</div>

<style type="scss">
  article:hover {
    color: var(--color);
  }

  div {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100%;
  }

  article {
    color: var(--primary);
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    padding: 1rem;
  }

  form {
    width: 450px;
    margin-top: 0.5rem;
  }
</style>
