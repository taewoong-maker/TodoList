<template>
    <div id="">
        <div id="TodoTitle">
            <span id="TodoTitleSpan">
                오늘 할 일
            </span>
        </div>
        <div id="TodoItems">
            <div class="TodoItem">
                123
            </div>
            <div class="TodoItem">
                123
            </div>
            <div class="TodoItem">
                123
            </div>
            <div class="TodoItem">
                123
            </div>
            <div class="TodoItem">
                123
            </div>

        </div>
    </div>
</template>

<script>
export default {
}
</script>

<style>
    #TodoTitle{
        margin: 5px 0;
    }
    #TodoItems{
        display: flex;
        flex-flow: nowrap column;
    }
    .TodoItem{
        background: rgb(226, 226, 226);
        border-radius: 10px;
        margin: 5px;
        font-size: 20px;
    }
</style>
