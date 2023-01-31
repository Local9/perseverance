<script lang="ts">
  import Icon from "@iconify/svelte";
</script>

<div class="tablet-background">
  <div class="tablet-header">
    <Icon icon="mdi:menu" class="tablet-header-icon" />
  </div>
</div>

<style lang="scss">
  .tablet-background {
    height: 100%;
    padding: 25px;
    border-radius: calc(var(--sizeVar) / 24);

    // display: flex;
    // flex-direction: column;
    // justify-content: center;
    // align-items: center;
    // height: 100%;
  }

  .tablet-background::before {
    content: "";
    background-image: url(https://i.imgur.com/jWVq2dL.jpeg);
    border-radius: calc(var(--sizeVar) / 24);
    background-size: cover;
    background-position: center;
    border: solid black calc(var(--sizeVar) / 200);
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
</style>
