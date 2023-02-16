<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import Input from '@components/form/inputs/Input.svelte';
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

<form on:submit|preventDefault={handleClientData}>
  <Input
    bind:value={username}
    type="text"
    name="username"
    placeholder="Username"
    aria-label="Username"
    autocomplete="nickname"
    required
  />
  <Input
    bind:value={password}
    type="password"
    name="password"
    placeholder="Password"
    aria-label="Password"
    autocomplete="current-password"
    required
  />
  <Input
    bind:value={passwordConfirm}
    type="password"
    name="passwordConfirm"
    placeholder="Confirm password"
    aria-label="Confirm password"
    autocomplete="current-password"
    required
  />
  <Input
    bind:value={registrationCode}
    type="password"
    name="registrationCode"
    placeholder="Registration code"
    aria-label="Registration code"
  />
  {#each errorMessages as message}
    <p>{message}</p>
  {/each}
  <Button type="submit" variant="default" size="md">Register</Button>
</form>

<style type="scss">
</style>
