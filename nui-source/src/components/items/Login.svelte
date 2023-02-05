<script lang="ts">
  import { authenticate } from '@store/auth';

  let username: string = '';
  let password: string = '';

  const handleClientData = () => {
    authenticate(username, password);
  };
</script>

<form on:submit|preventDefault={handleClientData}>
  <input
    bind:value={username}
    type="text"
    name="username"
    placeholder="Username"
    aria-label="Username"
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

<style type="scss">
</style>
