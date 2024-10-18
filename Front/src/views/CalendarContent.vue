<template>
    <div id="outerContent">
        <div id="calendarContent">
            <div id="SelectArea">
                <SelectBox id="YearSelect" class="select" @change="fnChangeSelected($event, 'year')"
                    :selectList="yearList" :selectedValue="selectedYear" ref="YearSelect" />
                <SelectBox id="MonthSelect" class="select" @change="fnChangeSelected($event, 'month')"
                    :selectList="monthList" :selectedValue="selectedMonth" ref="MonthSelect" />
            </div>
            <div>dev에서 커밋3</div>
            <table id="CalendarTable">
                <tbody>
                    <tr>
                        <td class="noHover">Sun</td>
                        <td class="noHover">Mon</td>
                        <td class="noHover">Tue</td>
                        <td class="noHover">Wed</td>
                        <td class="noHover">Thu</td>
                        <td class="noHover">Fri</td>
                        <td class="noHover">Sat</td>
                    </tr>
                    <tr v-for="weekItem in getMonthDates" :key="weekItem">
                        <td
                            v-for="dateItem in weekItem" :key="dateItem"
                            :data-date="dateItem" :ref="'date' + dateItem" @click="fnClickDateTd($event)"
                            :class="{ inDate: dateItem, 'todayDate': fnIsToday(dateItem) }">
                            {{dateItem}}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <div id="TodaysTodosContent" v-if="openTodoList">
            <TodaysTodos :Todays-todoList="todaysTodoList" name="hahah" @fnCompleteTodo="fnCompleteTodo" />
            <div id="AddTodoArea">
                <input type="text" id="addTodoInput" ref="addTodoInput" @keydown="fnAddTodoItemCheckKey($event)">
                <button id="addTodoBtn" @click.prevent="fnAddTodoItem">추가</button>
            </div>
        </div>
    </div>
</template>

<script>
// import CalendarDaysTd from './CalendarDaysTd.vue'
import TodaysTodos from './TodaysTodos.vue'
import SelectBox from '../components/SelectBox.vue'
//import axios from 'axios'

const WEEKDAY = ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'];
const TodoList = [
    { Number: '1', Date: '2022-03-15', TodoDetail: '할일1', Finished: false, Index: '1', CreatedDate: '2022-03-15'  },
    { Number: '2', Date: '2022-03-15', TodoDetail: '할일2', Finished: false, Index: '2', CreatedDate: '2022-03-15'  },
    { Number: '3', Date: '2022-03-15', TodoDetail: '할일3', Finished: false, Index: '3', CreatedDate: '2022-03-15'  },
    { Number: '4', Date: '2022-03-15', TodoDetail: '할일4', Finished: false, Index: '4', CreatedDate: '2022-03-15'  },
    { Number: '5', Date: '2022-03-15', TodoDetail: '할일5', Finished: false, Index: '5', CreatedDate: '2022-03-15'  },
    { Number: '6', Date: '2022-03-16', TodoDetail: '할일6', Finished: true, Index: '1', CreatedDate: '2022-03-15'  },
    { Number: '7', Date: '2022-03-16', TodoDetail: '할일7', Finished: true, Index: '2', CreatedDate: '2022-03-15'  },
    { Number: '8', Date: '2022-03-16', TodoDetail: '할일8', Finished: true, Index: '3', CreatedDate: '2022-03-15'  },
    { Number: '9', Date: '2022-03-16', TodoDetail: '할일9', Finished: false, Index: '4', CreatedDate: '2022-03-15'  },
    { Number: '10', Date: '2022-03-16', TodoDetail: '할일10', Finished: false, Index: '5', CreatedDate: '2022-03-15'  },
]

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
            selectedCalendarDate: '',
            startDay: 0,
            outDate: 'outDate dateTd',
            inDate: 'inDate dateTd',
            todaysTodoList: [],
            todayDate: new Date(),
            isThisMonth: false,
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
                    dateList[i][j] = this.fnAddZero(idx);
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
            const targetDate = e.currentTarget.dataset.date;
            const prevDate = this.selectedDate;
            if(prevDate === targetDate) {
                this.openTodoList = !this.openTodoList;
                this.selectedDate = -1;
                this.$refs['date' + targetDate][0].classList.remove('checked');
            } else {
                // 선택한 날짜가 기존 선택된 날짜와 다를 경우, 날짜를 처음 선택한 경우 todoList를 열어준다.
                this.selectedDate = targetDate;
                this.openTodoList = true;
                this.$refs['date' + targetDate][0].classList.add('checked');
                if(prevDate > 0) {
                    this.$refs['date' + prevDate][0].classList.remove('checked');
                }
            }
            this.selectedCalendarDate = this.fnGetDateFormat(this.selectedYear, this.selectedMonth, this.selectedDate);
            this.fnGetTodoItems();
        },
        fnChangeSelected(e, type) {
            if(type === 'year') {
                this.selectedYear = this.$refs.YearSelect.$el.value
            } else if(type === 'month') {
                this.selectedMonth = this.$refs.MonthSelect.$el.value
            }
            this.startDay = getStartDayIdx(this.selectedYear, this.selectedMonth);
            this.openTodoList = false;
        },
        fnGetTodoItems() {
            // axios.get('https://jsonplaceholder.typicode.com/users/')
            //     .then(function(response) {
            //         console.log(response);
            //     })
            //     .catch(function(error) {
            //         console.log(error);
            //     });
            this.todaysTodoList = [];
            TodoList.map((arr) => {
                if(arr.Date === this.selectedCalendarDate) {
                    this.todaysTodoList.push(arr);
                }
            });
        },
        fnAddTodoItem() {
            const todoDetail = this.$refs.addTodoInput.value;
            if(!todoDetail) {
                return false;
            }
            const todoItem = {
                Date: this.selectedCalendarDate,
                TodoDetail: todoDetail,
                Finished: false,
                CreatedDate: this.fnGetTodayDate()
            }
            TodoList.push(todoItem);
            this.$refs.addTodoInput.value = '';
            this.fnGetTodoItems();
        },
        fnAddTodoItemCheckKey(e) {
            if(e.keyCode === 13) {
                this.fnAddTodoItem();
            }
        },
        fnAddZero(item) {
            let _item;
            if(typeof (item) !== 'string') {
                _item = item.toString();
            } else {
                _item = item;
            }
            if(_item.length === 1) {
                return '0' + _item;
            } else {
                return _item;
            }
        },
        fnGetDateFormat(year, month, date) {
            return year + '-' + this.fnAddZero(month) + '-' + this.fnAddZero(date);
        },
        fnGetTodayDate() {
            const today = new Date();
            return this.fnGetDateFormat(today.getFullYear(), today.getMonth() + 1, today.getDate());
        },
        fnIsToday(param) {
            const date = new Date();
            const flag1 = this.selectedYear === date.getFullYear();
            const flag2 = this.selectedMonth === date.getMonth() + 1;
            
            let _param;
            typeof (param) === 'string' ? _param = parseInt(param) : _param = param;
            const flag3 = _param === date.getDate();
            if(flag1 && flag2 && flag3) {
                return true;
            }
        },
        fnCompleteTodo(param) {
            this.todaysTodoList[param.index].Finished = !param.flag;
        }
    },
    created() {
        const date = new Date();
        this.selectedYear = date.getFullYear();
        this.selectedMonth = date.getMonth() + 1;
        this.startDay = getStartDayIdx(this.selectedYear, this.selectedMonth);
    },
    mounted() {
    },
}
</script>

<style>
    #outerContent{
        display: flex;
        justify-content: center;
        flex-flow: nowrap row;
    }
    #CalendarTable{
        border-spacing: 0;
    }
    #CalendarTable td{
        border:none;
    }
    #CalendarTable td:not(.outDate, .noHover):hover{
        background-color: #f2f8fe;
    }
    #calendarContent{
        width: 50%;
    }
    #TodaysTodosContent{
        width: 35%;
        border: 1px solid;
    }
    .inDate{
        width: 70px;
        height: 70px;
        border: 1px solid;
        vertical-align: middle;
        cursor: pointer;
        margin: 0;
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
    .checked{
        background: #f2f8fe;
    }
    .todayDate{
        color:red;
    }
</style>
