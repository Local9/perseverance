<script lang="ts">
  // import { visibility } from "../store/stores";
  import { fetchNui } from "../utils/fetchNui";
  import "iconify-icon";

  let username: string;
  let password: string;

  const handleClientData = () => {
    fetchNui("authenticate", { 0: username, 1: password })
      .then((returnData) => {})
      .catch((e) => {});
  };

  // const closeDialog = (e: KeyboardEvent) => {
  //   if (e.type === "keydown" && e.key !== "Escape") return;
  //   visibility.set(false);
  //   fetchNui("hideUI");
  // };
</script>

<div>
  <form on:submit|preventDefault={handleClientData}>
    <input type="text" placeholder="Username" bind:value={username} />
    <input type="password" placeholder="Password" bind:value={password} />
    <button type="submit">Login</button>
  </form>
</div>

<style type="scss">
</style>
