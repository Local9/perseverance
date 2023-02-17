<script lang="ts">
  import Input from './inputs/Input.svelte';
  export let label: string = '';
</script>

<div class="{$$restProps.class} relative text-field flex flex-col mb-3">
  {#if label}
    <label for={$$restProps.id} class="mb-1 dark:text-white">
      {label}
    </label>
  {/if}
  <Input {...$$restProps} />
</div>
