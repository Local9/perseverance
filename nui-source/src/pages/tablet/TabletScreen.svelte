<script lang="ts">
  import Icon from "@iconify/svelte";
  import Citizens from "@citizen/Citizens.svelte";

  let activeTab = "citizens";
</script>

<div class="tablet-background">
  <div class="tablet-header">
    <Icon icon="mdi:menu" class="tablet-header-icon" />
  </div>
  <div class="tablet-container">
    {#if activeTab === "citizens"}
      <Citizens />
    {/if}
  </div>
  <div class="tablet-footer" />
</div>

<style lang="scss">
  .tablet-background {
    height: 100%;
    padding: 25px;
    border-radius: calc(var(--tablet-height-ratio) / 24);
    display: flex;
    flex-direction: column;
    background-color: var(--tablet-background-color);
    // justify-content: center;
    // align-items: center;
  }

  .tablet-background::before {
    content: "";
    background-image: url(https://i.imgur.com/51MkKwC.jpeg);
    border-radius: calc(var(--tablet-height-ratio) / 48);
    background-size: cover;
    background-position: center;
    border: solid black calc(var(--tablet-height-ratio) / 200);
    opacity: 0.25;
    position: absolute;
    top: 0;
    bottom: 0;
    right: 0;
    left: 0;
    width: auto;
    height: auto;
    z-index: -3;
  }

  .tablet-header {
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
  }

  .tablet-container {
    height: 100%;
    width: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    border-radius: 0.5rem;
    padding: 25px;
    display: flex;
    flex-direction: column;
    overflow-x: auto;
  }
</style>
