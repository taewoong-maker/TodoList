<template>
    <td >
        {{singleDate}}
    </td>
</template>

<script>
export default {
    props: ['date'],
    data() {
        return {
            singleDate: this.date
        }
    }
}
</script>
<style>

</style>
