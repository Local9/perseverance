<script lang="ts">
  import VisibilityProvider from "./providers/VisibilityProvider.svelte";
  import Login from "./components/Login.svelte";
  import { debugData } from "./utils/debugData";
  import "@picocss/pico";
  import "./app.scss";

  debugData([
    {
      action: "setVisible",
      data: true,
    },
  ]);
</script>

<main>
  <VisibilityProvider>
    <Login />
  </VisibilityProvider>
</main>
