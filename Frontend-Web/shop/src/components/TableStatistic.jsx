import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
function limit(c) {
    return this.filter((x, i) => {
      if (i <= c - 1) {
        return true;
      }
    });
  }
  Array.prototype.limit = limit;
  function skip(c) {
    return this.filter((x, i) => {
      if (i > c - 1) {
        return true;
      }
    });
  }
  Array.prototype.skip = skip;
function TableStatistic(props) {
  const { name, data, lables, setTime,timeSelect} = props;
  const [page, setPage] = useState(1);
  const [limit, setLimit] = useState(5);
  const [totalPage, setTotalPage] = useState(0);
  const [filter, setFilter] = useState('');
  const [datas, setDatas] = useState([]);

  useEffect(() => {
    let temp = data.filter((x) =>
      Object.values(x)
        .toString()
        .toLocaleLowerCase()
        .includes(filter.toString().toLocaleLowerCase())
    );
    setTotalPage(Math.ceil(temp.length / limit));
    setDatas(temp.skip((page - 1) * limit).limit(limit));
  }, [page, limit, data, filter]);

  const handleChangePage = (value) => {
    setPage(value);
  };
  const handleChangeLimitPage = (e) => {
    setLimit(e.target.value);
  };
  const handleChangeFilter = (e) => {
    setFilter(e.target.value);
  };
  return (
    <>
      <div className="table-statistic">
        <div className="header-table-statistic">
          <h3>{name} : <strong>{datas.length}</strong></h3>
          <div className="search-group">
            <input value={filter} type={'search'} onChange={handleChangeFilter} placeholder="Search" />
            <i className="fas fa-search"></i>
          </div>
          <select value={timeSelect} onChange={(e) => setTime(e.target.value)}>
            {' '}
            <option value={1}>This day</option>
            <option value={2}>This week</option>
            <option value={3}>This month</option>
          </select>
        </div>
        <div className="table">
          <table>
            <thead>
              <tr>
                {lables.map((lable, index) => (
                  <th>{lable}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {datas.map((data, index) => (
                <tr>
                  {Object.values(data).map((element, index) =>
                    index === Object.values(data).length - 1 ? (
                      <td>
                        {' '}
                        <a href={element} title="Go to detail"><i className="fas fa-info"></i></a>
                      </td>
                    ) : (
                      <td>{element}</td>
                    )
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}

export default TableStatistic;
