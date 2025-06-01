// Hàm lưu thông tin đặt phòng vào localStorage
function saveBookingInfo() {
    const checkIn = $('#date-range2').val();
    const checkOut = $('#date-range3').val();
    const adults = $('#adults').val() || '1';  // Default to '1' if not set
    const children = $('#children').val() || '1';  // Default to '1' if not set

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

        // Set date values
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
    // Initialize niceSelect
    $('#adults, #children').niceSelect();
    
    // Load thông tin đã lưu khi trang được tải
    setTimeout(function() {
        loadBookingInfo();
    }, 500);

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
    $('#adults, #children').on('change', function() {
        saveBookingInfo();
    });

    // Add event listener for calendar date selection
    $('#date-range12').on('datepicker-change', function(event, obj) {
        // Get selected dates
        const checkInDate = obj.date1;
        const checkOutDate = obj.date2;
        
        // Format dates as YYYY-MM-DD
        const formattedCheckIn = formatDate(checkInDate);
        const formattedCheckOut = formatDate(checkOutDate);
        
        // Update input fields
        $('#date-range2').val(formattedCheckIn);
        $('#date-range3').val(formattedCheckOut);
        
        // Save to localStorage
        saveBookingInfo();
        loadBookingInfo();
    });
});

// Helper function to format date as YYYY-MM-DD
function formatDate(date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}