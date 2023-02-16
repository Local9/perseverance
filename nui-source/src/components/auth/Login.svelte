<script lang="ts">
  import Button from '@components/form/Button.svelte';
  import Input from '@components/form/inputs/Input.svelte';
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

<form on:submit|preventDefault={handleClientData}>
  <Input
    bind:value={username}
    autoFocus
    type="text"
    name="username"
    placeholder="Username"
    aria-label="Username"
    autocomplete="nickname"
    required
    errorMessage={errorMessages['username']}
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
  <Button type="submit" variant="default" size="md">Login</Button>
</form>
