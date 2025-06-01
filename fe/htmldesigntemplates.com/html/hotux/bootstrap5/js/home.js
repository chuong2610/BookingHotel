// Hàm lưu thông tin đặt phòng vào localStorage
function saveBookingInfo() {
    const checkIn = $('#date-range2').val();
    const checkOut = $('#date-range3').val();
    const adults = $('#adults').val();
    const children = $('#children').val();

    const bookingInfo = {
        checkIn,
        checkOut,
        adults,
        children,
        timestamp: new Date().getTime()
    };

    localStorage.setItem('bookingInfo', JSON.stringify(bookingInfo));
}

// Hàm load thông tin đặt phòng từ localStorage
function loadBookingInfo() {
    const savedInfo = localStorage.getItem('bookingInfo');
    if (savedInfo) {
        const bookingInfo = JSON.parse(savedInfo);
        $('#date-range2').val(bookingInfo.checkIn);
        $('#date-range3').val(bookingInfo.checkOut);
        // Set select values
        if (bookingInfo.adults) {
            $('#adults').val(bookingInfo.adults).niceSelect('update');
        }

        if (bookingInfo.children) {
            $('#children').val(bookingInfo.children).niceSelect('update');
        }
    }
}

// Đợi document ready
$(document).ready(function() {
     $('#adults, #children').niceSelect();
    // Load thông tin đã lưu khi trang được tải
    loadBookingInfo();

    // Thêm event listener cho nút Check Availability
    $('.btn-check-availability').on('click', function(e) {
        e.preventDefault();
        saveBookingInfo();
        alert('Booking information saved!');
    });

    // Thêm event listener cho date picker
    $('#date-range2, #date-range3').on('datepicker-change', function() {
        saveBookingInfo();
    });

    // Thêm event listener cho select elements
    $('.wide').on('click', function() {
        setTimeout(saveBookingInfo, 100);
    });
});
