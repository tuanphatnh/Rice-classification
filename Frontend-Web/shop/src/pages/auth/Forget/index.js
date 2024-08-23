import classNames from 'classnames/bind';
import styles from './Login.module.scss';

import { useEffect, useRef, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { message } from 'antd';
import Loading from '~/components/Global/Loading';

const cx = classNames.bind(styles);

function ForgetPassword() {
    const navigate = useNavigate();
    const [isLoader, setIsLoader] = useState(false);
    const [email, setEmail] = useState('');
    const emailRef = useRef(null);
    const regEmailRef = useRef(null);

    const submitLogin = () => {
        if (validator()) {
            setIsLoader(true);
            axios
                .post(`https://localhost:44352/api/Auth/forgetPassword?email=${email}`)
                .then((res) => {
                    console.log(res);
                    if (res.status === 200) {
                        message.success('Successfully');
                        navigate('/login');
                        setIsLoader(false);
                    }
                })
                .catch((err) => {
                    message.error('Failed');
                    setIsLoader(false);
                });
        }
    };

    const validator = () => {
        if (!validateEmail(email)) {
            emailRef.current.focus();
            regEmailRef.current.style.display = 'inline';
            return false;
        } else {
            regEmailRef.current.style.display = 'none';
        }

        return true;
    };

    const validateEmail = (email) => {
        return email.match(
            /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
        );
    };

    const forgotPw = () => {};

    return (
        <div className={cx('wrap')}>
            <div className={cx('box')}>
                <h1 className="text-center text-4xl">Forgot Password</h1>
                <div className={cx('group')}>
                    <div className={cx('item')}>
                        <input
                            ref={emailRef}
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            type="email"
                            onKeyDown={(e) => e.key === 'Enter' && submitLogin()}
                            placeholder="Enter your email"
                        />
                        <span ref={regEmailRef} className={cx('item-log')} style={{ display: 'none' }}>
                            Please enter a valid email address
                        </span>
                    </div>
                    <div className={cx('item')}>
                        <button className={cx('btn')} onClick={() => submitLogin()}>
                            <span>Next</span>
                        </button>
                    </div>
                    <div className={cx('ruler-bar')}>
                        <div className={cx('ruler', 'left')}></div>
                        <span>Or</span>
                        <div className={cx('ruler', 'right')}></div>
                    </div>
                    <div className={cx('register') + ' mt-5'}>
                        <h4>
                            Already have an account?
                            <Link to="/login">
                                <span className="ml-3">Login now</span>
                            </Link>
                        </h4>
                    </div>
                </div>
            </div>
            {isLoader && <Loading />}
        </div>
    );
}

export default ForgetPassword;
