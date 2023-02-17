<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import TextField from '@components/form/TextField.svelte';
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
    register(username, password, passwordConfirm, registrationCode);
  };
</script>

<form on:submit|preventDefault={handleClientData} class={$$restProps.class}>
  <TextField
    bind:value={username}
    label="Username"
    type="text"
    name="username"
    placeholder="Username"
    aria-label="Username"
    autocomplete="nickname"
    required
  />
  <TextField
    bind:value={password}
    label="Password"
    type="password"
    name="password"
    placeholder="Password"
    aria-label="Password"
    autocomplete="current-password"
    required
  />
  <TextField
    bind:value={passwordConfirm}
    label="Confirm password"
    type="password"
    name="passwordConfirm"
    placeholder="Confirm password"
    aria-label="Confirm password"
    autocomplete="current-password"
    required
  />
  <TextField
    bind:value={registrationCode}
    label="Registration code"
    type="password"
    name="registrationCode"
    placeholder="Registration code"
    aria-label="Registration code"
  />
  {#each errorMessages as message}
    <p>{message}</p>
  {/each}
  <Button
    type="submit"
    variant="default"
    size="md"
    class="flex items-center justify-center w-full gap-3 mt-5">Register</Button
  >
</form>

<style type="scss">
</style>
