import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import {
  AiOutlineDelete,
  AiOutlineEdit,
  AiOutlineSearch,
} from 'react-icons/ai';

import { MdAdd, MdNumbers } from 'react-icons/md';
import { instance } from '../config/axiosConfig';
import Pagination from './Pagination';
import CloudinaryUploadWidget from './CloudinaryUploadWidget';
import { Link } from 'react-router-dom';
import RoleFilter from './RoleFilter';

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
function addDays(date, days) {
  const dateCopy = new Date(date);
  dateCopy.setDate(date.getDate() - days);
  return dateCopy;
}
function TableSearch(props) {
  const {
    buttonActive,
    setValue,
    value,
    setRoleId,
    roleId,
    actions,
    setInfo,
    valueAction,
    lable,
    setReloaded,
    data,
    initValues,
    types,
    readOnly,
    setDataInfo,
    addRole,setAddRole
  } = props;
  const [edit, setEdit] = useState(false);
  const [datas, setDatas] = useState([]);
  const [add, setAdd] = useState(false);
  const [stateAll, setSateAll] = useState(false);
  const [arraySelect, setArraySelect] = useState([]);
  const keys = Object.keys(initValues);
  const [page, setPage] = useState(1);
  const [limit, setLimit] = useState(5);
  const [totalPage, setTotalPage] = useState(0);
  const [filter, setFilter] = useState('');

  useEffect(() => {
    let temp = data.filter((x) =>
      Object.values(x)
        .toString()
        .toLocaleLowerCase()
        .includes(filter.toString().toLocaleLowerCase())
    );
    setTotalPage(Math.ceil(temp.length / limit));
    setArraySelect(new Array(temp.length).fill(false));
    setDatas(temp.skip((page - 1) * limit).limit(limit));
  }, [page, limit, data, filter]);

  const handleChangePage = (value) => {
    setPage(value);
  };
  const handleChangeLimitPage = (e) => {
    setLimit(e.target.value);
  };


  const handleSelectAll = () => {
    setSateAll(!stateAll);
    setAdd(false);
    stateAll
      ? setArraySelect(arraySelect.map((x) => (x = false)))
      : setArraySelect(arraySelect.map((x) => (x = true)));
  };
  const handleChangeSelect = async (data, index) => {
    let array = arraySelect.map((array, id) =>
      id === Number(index + (page - 1) * limit) ? !array : array
    );
    setArraySelect(array);
     setValue(Object.values(data));
    setAdd(false);

    if(addRole){
      instance.post(`${actions.editUserRole}${roleId}`,[{
        userName: data.userName,
        userId: data.userId,
        isSelected: !data.isSelected
      }]).then (rs => {toast.success(rs.data)
      
      setReloaded()}
      )
      }
   
  };
  const handleImage = (url) => {
    let temp = value.map(
      (value, index) => (value = types[index] === 'image' ? url : value)
    );
    setValue(temp);
  };
  const handleChangeEdit = (e) => {
    let values = value.map(
      (value, index) =>
        (value = index === Number(e.target.name) ? e.target.value : value)
    );
    setValue(values);
  };
  const handleOnsubmit = (e) => {
    e.preventDefault();
    if (value[0] === 0 || value[0] === '') {
      instance
        .post(actions.post, valueAction)
        .then((rs) => {
          if (rs.data.isSuccess || rs.data.succeeded) {
            toast.success(rs.data.message ===null ? rs.data.message : "Added");
            setReloaded();
            setAdd(false);
            setValue(Object.values(initValues));
          } else toast.error(rs.data.errors[0]);
        })
        .catch((e) => console.log(e));
    } else {
      if (actions.put.toString().includes('edit')) {
        instance
          .post(`${actions.put}`, valueAction)
          .then((rs) => {
            if (rs.data.isSuccess || rs.data.succeeded) {
              toast.success(rs.data.message ===null ? rs.data.message : "Edited");
              setArraySelect(arraySelect.map((x) => (x === true ? !x : x)));
              setReloaded();
            } else toast.error(rs.data.errors[0]);
          })
          .catch((e) => console.log(e));
      } else {
        instance
          .put(`${actions.put}${valueAction.id}`, valueAction)
          .then((rs) => {
            if (rs.data.isSuccess) {
              toast.success(rs.data.message);
              setArraySelect(arraySelect.map((x) => (x === true ? !x : x)));
              setReloaded();
            } else toast.error(rs.data.errors[0]);
          })
          .catch((e) => console.log(e));
      }
    }
  };
  const handleDelete = () => {
    let idDelete = 0;
    arraySelect.map((v, i) => {
      if (v === true) idDelete = data[i].id;
    });
    if (
      window.confirm('Are you sure DELETE items have ID : ' + idDelete) === true
    ) {
      instance
        .delete(`${actions.delete}${idDelete}`)
        .then((rs) => {
          if (rs.data.isSuccess) {
            toast.success(rs.data.message);
            setReloaded();
          } else {
            toast.error(rs.data.message);
          }
        })
        .catch((e) => console.log(e));
    }
  };
  const handleChangeFilter = (e) => {
    setFilter(e.target.value);
  };
  const handleInfo = (value) =>{
    setInfo()
    setDataInfo(value)
  }
  return (
    <>
      <ToastContainer
        position="top-center"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
      <div className="search-table">
        <div className="search-group">
          <i className="icon">
            <AiOutlineSearch />
          </i>
          <input
            type={'search'}
            value={filter}
            onChange={handleChangeFilter}></input>
        </div>
        <div className="tool">
          <button className="num">
            <span>
              <MdNumbers className="icon" />
              {datas.length}
            </span>
          </button>
          {buttonActive == undefined  && <button
            className="add"
            onClick={() => {
              setAdd(!add);
              setValue(Object.values(initValues));
            }}>
            <span>
              <MdAdd className="icon" />
              Add
            </span>
          </button>}
            {roleId !== undefined && <RoleFilter addRole={addRole} setAddRole={setAddRole} roleId = {roleId} setRoleId={setRoleId} />}
          {buttonActive === undefined  && <button className="delete" onClick={() => handleDelete()}>
            <AiOutlineDelete className="icon" />
            Delete
          </button>}
          {buttonActive == undefined  && <button className="tool-select">
            <AiOutlineEdit className="icon" />
            Edit
            <label class="switch">
              <input
                type="checkbox"
                checked={edit}
                disabled={
                  stateAll || arraySelect.filter((x) => x === true).length > 1
                }
                onChange={() => {
                  setEdit(!edit);
                  setAdd(false);
                }}
              />
              <span class="slider-button round"></span>
            </label>
          </button>}
        </div>
      </div>
      {add && (
        <form className="form-add" onSubmit={handleOnsubmit}>
          {keys.map((key, index) =>
            index !== 0 && !readOnly[index] ? (
              <div className="form-group">
                <label>{lable[index]}</label>
                {types[index] === 'image' ? (
                  <CloudinaryUploadWidget onHandle={handleImage} />
                ) : (
                  <input
                    type={types[index]}
                    name={index}
                    value={value[index]}
                    onChange={handleChangeEdit}
                    readOnly={readOnly[index]}></input>
                )}
              </div>
            ) : (
              ''
            )
          )}
          <button type="submit">Add</button>
        </form>
      )}
      {datas.length === 0 ? (
        <div className="non-data">
          <img
            className="img-error"
            src="https://cdn-icons-png.flaticon.com/512/20/20093.png"
            alt=""
          />
          <p>No database</p>
        </div>
      ) : (
        <div className="table">
          <table>
            <thead>
              <tr>
                <th>
                  <input
                    disabled={edit}
                    checked={
                      stateAll || arraySelect.every((x) => x === true)
                        ? true
                        : false
                    }
                    type={'checkbox'}
                    onClick={handleSelectAll}></input>
                </th>
                {lable.map((lb, id) => (
                  <th key={id}>{lb}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {datas.map((data, index) => (
                <>
                  <tr
                    className={
                      arraySelect[index + (page - 1) * limit] ? 'trcheck' : ''
                    }
                    key={data.key}>
                    <td>
                      {' '}
                      <input
                        name={index}
                        checked={addRole ? data.isSelected :  arraySelect[index + (page - 1) * limit] }
                        type={'checkbox'}
                        disabled={arraySelect.find(
                          (x, id) =>
                            id !== index + (page - 1) * limit && x === true
                        )}
                        onClick={() => handleChangeSelect(data, index)}></input>
                    </td>
                    {Object.values(data).map((values, index) => (
                      lable[index] !== undefined &&
                      <td>

                        {types[index] === 'image' && values!==null? (
                          <img
                            alt=""
                            className="img-table"
                            src={values.toString()}
                          />
                        ) : values === null ? (
                          ''
                        ) : types[index] === 'date' ? (
                          new Date(Date.parse(values)).toLocaleDateString()
                        ) : types[index] === 'number' ? (
                          Intl.NumberFormat('vi-VN', {
                            maximumSignificantDigits: 3,
                          }).format(Number.parseInt(values.toString()))
                        ) : types[index] === 'color' ? (
                          <span
                            style={{ backgroundColor: values }}
                            className="color-review"></span>
                        ) : types[index] ==='profile' ?
                        <Link to={`${actions.getProfile}?${values}`} title="go to profile" > <i style={{textAlign:'center', fontSize: '2rem'}} >{'--->'}</i></Link>
                       
                        : types[index]==='more' ?
                        
                        <i title='info' onClick={()=>handleInfo(values)} className='fas fa-info-circle' style={{textAlign:'center', fontSize: '2rem', color:'green'}} ></i>
                        :(
                          values.toString() 
                        )}
                      </td>
                    ))}
                  </tr>
                  {edit && arraySelect[index] && (
                    <tr className={arraySelect[index] ? 'trcheck' : ''}>
                      <td colSpan={lable.length + 1}>
                        <form className="form-edit" onSubmit={handleOnsubmit}>
                          {keys.map((key, index) =>
                            !readOnly[index] ? (
                              <div className="form-group">
                                <label>{lable[index]}</label>
                                {types[index] === 'image' ? (
                                  <CloudinaryUploadWidget
                                    oldSrc={value[index]}
                                    onHandle={handleImage}
                                  />
                                ) : (
                                  <input
                                    type={types[index]}
                                    name={index}
                                    value={value[index]}
                                    onChange={handleChangeEdit}
                                    readOnly={readOnly[index]}></input>
                                )}
                              </div>
                            ) : (
                              ''
                            )
                          )}
                          <button type="submit">Save</button>
                        </form>
                      </td>
                    </tr>
                  )}
                </>
              ))}
            </tbody>
          </table>
        </div>
      )}
      {datas.length !== 0 && (
        <Pagination
          page={page}
          limit={limit}
          totalPage={totalPage}
          handleChangePage={handleChangePage}
          handleChangeLimitPage={handleChangeLimitPage}
          siblings={1}
        />
      )}
    </>
  );
}

export default TableSearch;
