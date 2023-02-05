<script lang="ts">
  import { register } from '@store/auth';

  let username: string = '';
  let password: string = '';
  let passwordConfirm: string = '';
  let registrationCode: string = '';

  let errorMessages: string[] = [];

  const handleClientData = () => {
    if (password !== passwordConfirm) {
      errorMessages.push('Passwords do not match');
      return;
    }
    register(username, password);
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
  <input
    bind:value={passwordConfirm}
    type="password"
    name="passwordConfirm"
    placeholder="Confirm password"
    aria-label="Confirm password"
    autocomplete="current-password"
    required
  />
  <input
    bind:value={registrationCode}
    type="password"
    name="registrationCode"
    placeholder="Registration code"
    aria-label="Registration code"
  />
  {#each errorMessages as message}
    <p>{message}</p>
  {/each}
  <button type="submit" class="contrast">Register</button>
</form>

<style type="scss">
</style>
