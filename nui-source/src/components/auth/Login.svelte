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
    class={`w-full p-1.5 px-3 bg-white rounded-md border outline-none dark:bg-secondary dark:text-white disabled:cursor-not-allowed disabled:opacity-80 placeholder:opacity-50`}
  />
  <input
    bind:value={password}
    type="password"
    name="password"
    placeholder="Password"
    aria-label="Password"
    autocomplete="current-password"
    required
    class={`w-full p-1.5 px-3 bg-white rounded-md border outline-none dark:bg-secondary dark:text-white disabled:cursor-not-allowed disabled:opacity-80 placeholder:opacity-50`}
  />
  <button
    type="submit"
    class="rounded-md disabled:opacity-60 disabled:cursor-not-allowed transition-colors"
    >Login</button
  >
</form>
