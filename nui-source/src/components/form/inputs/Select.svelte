<script lang="ts">
  export let label: string = '';
  export let optionalLabel: string = '';
  export let value: any = '';
  export let options: { value: any; label: string }[] = [];
  export let errorMessage: string = '';
</script>

<div class="{$$restProps.class} relative text-field flex flex-col mb-3">
  {#if label}
    <label for={$$restProps.id} class="mb-1 dark:text-white">
      {label}
      {#if optionalLabel}
        <span class="text-gray-500 dark:text-gray-400"> ({optionalLabel})</span>
      {/if}
    </label>
  {/if}
  <select
    bind:value
    {...$$restProps}
    class="w-full p-1.5 px-3 bg-white rounded-md border
    outline-none
    dark:bg-secondary dark:text-white
    disabled:cursor-not-allowed disabled:opacity-80
    placeholder:opacity-50
    transition-all {errorMessage ? 'border-red-500 focus:border-red-700 dark:focus:border-red-700' : 'border-gray-200 dark:border-quinary focus:border-gray-800 dark:focus:border-gray-500'}}"
  >
    {#each options as option}
      <option value={option.value}>{option.label}</option>
    {/each}
  </select>
</div>
