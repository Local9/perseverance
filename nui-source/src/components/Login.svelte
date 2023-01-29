<script lang="ts">
  import { visibility } from "../store/stores";
  import { fetchNui } from "../utils/fetchNui";
  import "iconify-icon";

  const handleClientData = () => {
    fetchNui("getClientData")
      .then((returnData) => {})
      .catch((e) => {});
  };

  const closeDialog = () => {
    visibility.set(false);
    fetchNui("hideUI");
  };
</script>

<div>
  <iconify-icon
    class="close"
    icon="mdi:close"
    width="24"
    height="24"
    on:click={closeDialog}
  />
</div>

<style>
</style>
