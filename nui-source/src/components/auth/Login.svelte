<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import TextField from '@components/form/TextField.svelte';
  import { authenticate } from '@store/auth';

  let username: string = '';
  let password: string = '';

  let errorMessages: string[] = [];

  const handleClientData = () => {
    errorMessages = [];
    if (!authenticate(username, password)) {
      errorMessages['username'] = 'Username or password is incorrect';
    }
  };
</script>

<form on:submit|preventDefault={handleClientData} class={$$restProps.class}>
  <TextField
    bind:value={username}
    label="Username"
    autoFocus
    type="text"
    name="username"
    placeholder="Username"
    aria-label="Username"
    autocomplete="nickname"
    required
    errorMessage={errorMessages['username']}
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
  <Button
    type="submit"
    variant="default"
    size="md"
    class="flex items-center justify-center w-full gap-3 mt-5">Login</Button
  >
</form>
