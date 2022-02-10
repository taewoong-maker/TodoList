<template>
    <div id="outerContent">
        <div id="calendarContent">
            <div id="SelectArea">
                <SelectBox id="YearSelect" class="select" @change="fnChangeSelected($event, 'year')"
                    :selectList="yearList" :selectedValue="selectedYear" ref="YearSelect" />
                <SelectBox id="MonthSelect" class="select" @change="fnChangeSelected($event, 'month')"
                    :selectList="monthList" :selectedValue="selectedMonth" ref="MonthSelect" />
            </div>
            <table id="CalendarTable">
                <tbody>
                    <tr v-for="weeks in getMonthDates" :key="weeks">
                        <!-- <CalendarDaysTd :class="date ? dateTd : noData" v-for="date in weeks" :key="date"
                            :date="date" @click="fnClickDateTd" /> -->
                        <td :class="date ? dateTd : noData" v-for="date in weeks" :key="date"
                            :data-date="date" @click="fnClickDateTd($event)">
                            {{date}}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="TodaysTodosContent" v-if="openTodoList">
            <TodaysTodos />
            <div id="AddTodoArea">
                <input type="text" id="addTodoInput" ref="addTodoInput">
                <button id="addTodoBtn" @click.prevent="addTodoItem">추가</button>
            </div>
        </div>
    </div>

</template>

<script>
// import CalendarDaysTd from './CalendarDaysTd.vue'
import TodaysTodos from './TodaysTodos.vue'
import SelectBox from '../components/SelectBox.vue'
import axios from 'axios'
                                     
const WEEKDAY = ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'];

function daysInMonth (month, year) {
    return new Date(year, month, 0).getDate();
}

function getStartDayIdx (year, month) {
    if(month < 2) {
        month = '0' + month
    }
    const startDate = new Date(`${year}-${month}-01`);
    return startDate.getDay();
}

export default {
    data() {
        return {
            monthDays: [],
            openTodoList: false,
            yearList: [2019, 2020, 2021, 2022, 2023],
            monthList: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
            selectedYear: 0,
            selectedMonth: 0,
            selectedDate: 0,
            startDay: 0,
            noData: 'noData',
            dateTd: 'dateTd',
            todaysTodoList: []
        }
    },
    computed: {
        getMonthDates: function () {
            const thisYear = this.selectedYear;
            const thisMonth = this.selectedMonth;
            const dates = daysInMonth(thisMonth, thisYear)
            const dateList = [];

            let idx = 1;
            let i = 0;
            let j = 0;
            dateList.push([]);
            let startDayIdx = this.startDay;
            while(dates >= idx) {
                if(startDayIdx > 0) {
                    dateList[i][j] = null;
                    startDayIdx--;
                } else {
                    dateList[i][j] = idx;
                    idx++;
                }

                if(j === 6) {
                    dateList.push([]);
                    j = 0;
                    i++;
                } else {
                    j++;
                }
            }
            return dateList;
        }
    },
    components: {
        TodaysTodos,
        SelectBox,
    },
    methods: {
        fnClickDateTd(e) {
            if(this.selectedDate === e.currentTarget.dataset.date) {
                this.openTodoList = !this.openTodoList;
            } else {
                // 선택한 날짜가 기존 선택된 날짜와 다를 경우, 날짜를 처음 선택한 경우 todoList를 열어준다.
                this.selectedDate = e.currentTarget.dataset.date;
                this.openTodoList = true;
            }
        },
        fnChangeSelected(e, type) {
            if(type === 'year') {
                this.selectedYear = this.$refs.YearSelect.$el.value
            } else if(type === 'month') {
                this.selectedMonth = this.$refs.MonthSelect.$el.value
            }
            this.startDay = getStartDayIdx(this.selectedYear, this.selectedMonth);
        },
        addTodoItem() {
            const todoItem = this.$refs.addTodoInput.value;
            axios.get('https://jsonplaceholder.typicode.com/users/')
                .then(function(response) {
                console.log(response);
                })
                .catch(function(error) {
                console.log(error);
                });
        }
    },
    created() {
        const date = new Date();
        this.selectedYear = date.getFullYear();
        this.selectedMonth = date.getMonth() + 1;
        this.startDay = getStartDayIdx(this.selectedYear, this.selectedMonth);
    },
}
</script>

<style>
    #outerContent{
        display: flex;
        justify-content: center;
        flex-flow: nowrap row;
    }
    #calendarContent{
        width: 50%;
    }
    #TodaysTodosContent{
        width: 35%;
        border: 1px solid;
    }
    #TodaysTodosContent>div{
        height: 90%;
    }
    .dateTd{
        width: 70px;
        height: 70px;
        border: 1px solid;
        vertical-align: middle;
    }
    .noData{
        border: none;
    }
    .select{
        display: inline-block;
        margin: 10px 10px;
    }
    .select select{
        font-size: 15px;
    }
    #addTodoInput, #addTodoBtn{
        margin: 5px;
    }
    #addTodoInput{
        height: 20px;
    }
    #addTodoBtn{
        border: none;
        height: 30px;
        width: 50px;
        background-color: #6cafce;
        border-radius: 5px;
        color: white;
        font-weight: bold;
        cursor: pointer;
    }
</style>
