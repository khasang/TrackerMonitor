var tabList = [
    { 'id': 1, 'name': 'Профайл'},
    { 'id': 2, 'name': 'Мониторинг'},
    { 'id': 3, 'name': 'Трекеры'},
    { 'id': 4, 'name': 'Группы'}
];

ReactDOM.render(
    <App tabList={tabList} trackers={trackers}/>,
    document.getElementById('container')
);