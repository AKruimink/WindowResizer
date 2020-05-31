﻿using System;
using Resizer.Gui.Helpers.Messenger;
using Xunit;

namespace Resizer.Gui.Tests.Helpers.Messenger
{
    /// <summary>
    /// Tests for the <see cref="EventSubscription"/>
    /// </summary>
    public class EventSubscriptionTests
    {
        #region Test Fixtures

        private class MockDelegateReference : IDelegateReference
        {
            public Delegate? Delegate { get; set; }

            public MockDelegateReference()
            {

            }

            public MockDelegateReference(Delegate @delegate)
            {
                Delegate = @delegate;
            }
        }

        #endregion

        #region Constructor Tests

        [Fact]
        public void ConstructNonGeneric_NullActionReference_ShouldThrowArgumentNullException()
        {
            // Prepare

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription(null!);
            });
        }

        [Fact]
        public void ConstructGeneric_NullActionReference_ShouldThrowArgumentNullException()
        {
            // Prepare
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(null!, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructGeneric_NullFilterReference_ShouldThrowArgumentNullException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { } };

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, null!);
            });
        }

        [Fact]
        public void ConstructNonGeneric_NullAction_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = null };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription(mockActionReference);
            });
        }

        [Fact]
        public void ConstructGeneric_NullAction_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = null };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructGeneric_NullFilter_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { }};
            var mockFilterReference = new MockDelegateReference() { Delegate = null };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructNonGeneric_DifferentTargetTypeActionReference_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<int>)delegate { } };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription(mockActionReference);
            });
        }

        [Fact]
        public void ConstructGeneric_DifferentTargetTypeActionReference_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<int>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        [Fact]
        public void ConstructGeneric_DifferentTargetTypeFilterReference_ShouldThrowArgumentException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<int>)(arg => { return true; }) };

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            });
        }

        #endregion

        #region SubscriptionToken Tests

        [Fact]
        public void SubscriptionTokenNonGeneric_Intialize_ShouldInitialize()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate { } };
            var eventSubscription = new EventSubscription(mockActionReference);
            var subscriptionToken = new SubscriptionToken();

            // Act
            eventSubscription.SubscriptionToken = subscriptionToken;

            // Assert
            Assert.Same(mockActionReference.Delegate, eventSubscription.GetAction);
            Assert.Same(subscriptionToken, eventSubscription.SubscriptionToken);
        }

        [Fact]
        public void SubscriptionTokenGeneric_Intialize_ShouldInitialize()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)(arg => { return true; }) };
            var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            var subscriptionToken = new SubscriptionToken();

            // Act
            eventSubscription.SubscriptionToken = subscriptionToken;

            // Assert
            Assert.Same(mockActionReference.Delegate, eventSubscription.GetAction);
            Assert.Same(mockFilterReference.Delegate, eventSubscription.GetFilter);
            Assert.Same(subscriptionToken, eventSubscription.SubscriptionToken);
        }

        #endregion

        #region GetAction Tests

        [Fact]
        public void GetActionNonGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate { } };
            var eventSubscription = new EventSubscription(mockActionReference);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            mockActionReference.Delegate = null;
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetActionGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)(arg => { return true; }) };
            var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            mockActionReference.Delegate = null;
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        #endregion

        #region GetExecutionStrategy Tests

        [Fact]
        public void GetExecutionStrategyGeneric_PassArgument_ShouldPassArgumentToDelegates()
        {
            // Prepare
            string? passedArgumentToAction = null;
            string? passedArgumentToFilter = null;
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<string>)(obj => passedArgumentToAction = obj) };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<string>)(obj => { passedArgumentToFilter = obj; return true; }) };
            var eventSubscription = new EventSubscription<string>(mockActionReference, mockFilterReference);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(new[] { "TestString" });

            // Assert
            Assert.Equal("TestString", passedArgumentToAction);
            Assert.Equal("TestString", passedArgumentToFilter);
        }

        [Fact]
        public void GetExecutionStrategyNonGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate {  } };
            var eventSubscription = new EventSubscription(mockActionReference);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            mockActionReference.Delegate = null;
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetExecutionStrategyGeneric_NullAction_ShouldReturnNull()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<int>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<int>)delegate { return false; } };
            var eventSubscription = new EventSubscription<int>(mockActionReference, mockFilterReference);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            mockActionReference.Delegate = null;
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetExecutionStrategyGeneric_NullFilter_ShouldReturnNull()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<int>)delegate { } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<int>)delegate { return false; } };
            var eventSubscription = new EventSubscription<int>(mockActionReference, mockFilterReference);
            Action<object[]>? publishAction;

            // Act
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.NotNull(publishAction);

            // Act
            mockFilterReference.Delegate = null;
            publishAction = eventSubscription.GetExecutionStrategy();

            // Assert
            Assert.Null(publishAction);
        }

        [Fact]
        public void GetExecutionStrategyGeneric_FilterReturnsFalse_ShouldNotExecute()
        {
            // Prepare
            var actionExecuted = false;
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<int>)delegate { actionExecuted = true; } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<int>)delegate { return false; } };
            var eventSubscription = new EventSubscription<int>(mockActionReference, mockFilterReference);
            var publishAction = eventSubscription.GetExecutionStrategy();

            // Act
            publishAction?.Invoke(new object[] { null! });

            // Assert
            Assert.False(actionExecuted);
        }

        #endregion

        #region InvokeAction Tests

        [Fact]
        public void InvokeActionNonGeneric_ValidAction_ShouldExecuteAction()
        {
            // Prepare
            var actionExecuted = false;
            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate { actionExecuted = true; } };
            var eventSubscription = new EventSubscription(mockActionReference);

            // Act
            eventSubscription.InvokeAction((Action)mockActionReference.Delegate);

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void InvokeActionGeneric_ValidAction_ShouldExecuteAction()
        {
            // Prepare
            var actionExecuted = false;
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate { actionExecuted = true; } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)delegate { return false; } };
            var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);

            // Act
            eventSubscription.InvokeAction((Action<object>)mockActionReference.Delegate, "testString");

            // Assert
            Assert.True(actionExecuted);
        }

        [Fact]
        public void InvokeActionNonGeneric_NullAction_ShouldThrowArgumentNullException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action)delegate { } };
            var eventSubscription = new EventSubscription(mockActionReference);

            // Act
            

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                eventSubscription.InvokeAction(null!);
            });
        }

        [Fact]
        public void InvokeActionGeneric_NullAction_ShouldThrowArgumentNullException()
        {
            // Prepare
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<object>)delegate {  } };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<object>)delegate { return true; } };
            var eventSubscription = new EventSubscription<object>(mockActionReference, mockFilterReference);

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                eventSubscription.InvokeAction(null!, "testString");
            });
        }

        [Fact]
        public void InvokeActionGeneric_PassArgument_ShouldPassArgument()
        {
            // Prepare
            string? passedArgument = null;
            var mockActionReference = new MockDelegateReference() { Delegate = (Action<string>)(obj => passedArgument = obj) };
            var mockFilterReference = new MockDelegateReference() { Delegate = (Predicate<string>)delegate { return true; } };
            var eventSubscription = new EventSubscription<string>(mockActionReference, mockFilterReference);

            // Act
            eventSubscription.InvokeAction((Action<string>)mockActionReference.Delegate, "someString");

            // Assert
            Assert.Equal("someString", passedArgument);
        }

        #endregion
    }
}