<script type="ts">
  export let variant:
    | 'default'
    | 'cancel'
    | 'danger'
    | 'success'
    | 'transparent'
    | 'link'
    | 'blue'
    | 'amber' = 'default';
  export let size: 'xs' | 'sm' | 'md' | 'lg' = 'md';

  export const buttonVariants = {
    default:
      'bg-gray-300 hover:bg-gray-400 text-black dark:hover:brightness-150 dark:bg-secondary dark:text-white border border-gray-400 dark:border-quinary',
    cancel:
      'bg-transparent hover:bg-transparent text-gray-800 dark:text-gray-200',
    danger: 'bg-red-500 hover:bg-red-600 border border-red-800 text-black',
    success:
      'bg-emerald-500 hover:bg-emerald-600 border border-emerald-800 text-black',
    transparent: 'bg-transparent text-black dark:text-white',
    link: 'bg-transparent hover:bg-transparent text-gray-800 dark:text-gray-200 px-0 p-1 underline',
    blue: 'bg-blue-500 dark:bg-blue-500 hover:bg-blue-600 dark:hover:bg-blue-600 dark:border dark:border-blue-800 text-black',
    amber:
      'text-black bg-orange-500 dark:bg-orange-500 hover:bg-orange-600 dark:hover:bg-orange-600',
  } as const;

  export const buttonSizes = {
    xs: 'p-0.5 px-2',
    sm: 'p-1 px-4',
    md: 'p-1.5 px-4',
    lg: 'p-2 px-6',
  } as const;
</script>

<button
  on:click
  on:mouseover
  on:mouseenter
  on:mouseleave
  on:focus
  class="
  {$$restProps.class}
  {variant}
  {buttonVariants[variant]}
  {buttonSizes[size]}
  rounded-md disabled:opacity-60 disabled:cursor-not-allowed transition-colors w-full"
>
  <slot />
</button>
