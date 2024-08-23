import classNames from 'classnames/bind';
import styles from './Search.module.scss';
import { SearchOutlined } from '@ant-design/icons';
import { useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const cx = classNames.bind(styles);

function Search() {
    const [inputValue, setInputValue] = useState('');
    const inputRef = useRef(null);
    const navigate = useNavigate();

    const Search = () => {
        navigate(`/news?search=${inputValue}`);
    };

    return (
        <div className={cx('wrap')}>
            <input
                className={cx('input')}
                type="text"
                value={inputValue}
                ref={inputRef}
                onChange={(e) => setInputValue(e.target.value)}
                onKeyDown={(e) => {
                    if (e.key === 'Enter') Search();
                }}
            />
            <div className={cx('btn-search')} onClick={() => Search()}>
                <SearchOutlined classname={cx('icon')} />
            </div>
        </div>
    );
}

export default Search;
